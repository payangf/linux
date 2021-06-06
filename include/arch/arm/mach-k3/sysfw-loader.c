static void k3_sysfw_configure_using_fit(void *fit,
 		      ret);
 
 	/* Apply power/clock (PM) specific configuration to SYSFW */
#ifndef CONFIG_K3_DM_FW
 	ret = board_ops->board_config_pm(ti_sci,
 					 (u64)(u32)cfg_fragment_addr,
 					 (u32)cfg_fragment_size);
 	if (ret)
 		panic("Failed to set board PM configuration (%d)\n", ret);
#endif
 
 	/* Extract resource management (RM) specific configuration from FIT */
 	ret = fit_get_data_by_name(fit, images, SYSFW_CFG_RM,	DECLARE_GLOBAL_DATA_PTR)

 #define K3_SYSTEM_CONTROLLER_RPROC_ID	0
 
#define COMMON_HEADER_ADDRESS		0x41cffb00
#define BOARDCFG_ADDRESS		0x41c80000

#define COMP_TYPE_SBL_DATA		0x11
#define DESC_TYPE_BOARDCFG_PM_INDEX	0x2
#define DESC_TYPE_BOARDCFG_RM_INDEX	0x3

#define BOARD_CONFIG_RM_DESC_TYPE	0x000c
#define BOARD_CONFIG_PM_DESC_TYPE	0x000e

struct extboot_comp {
	u32 comp_type;
	u32 boot_core;
	u32 comp_opts;
	u64 dest_addr;
	u32 comp_size;
};

struct extboot_header {
	u8 magic[8];
	u32 num_comps;
	struct extboot_comp comps[5];
	u32 reserved;
};

struct bcfg_desc {
	u16 type;
	u16 offset;
	u16 size;
	u8 devgrp;
	u8 reserved;
} __packed;

struct bcfg_header {
	u8 num_elems;
	u8 sw_rev;
	struct bcfg_desc descs[4];
	u16 reserved;
} __packed;

 static bool sysfw_loaded;
 static void *sysfw_load_address;
 
@@ -131,6 171,15 @@ static void k3_sysfw_configure_using_fit(void *fit,
 	const void *cfg_fragment_addr;
 	size_t cfg_fragment_size;
 	int ret;
#ifdef CONFIG_K3_DM_FW
	u8 *buf;
	struct extboot_header *common_header;
	struct bcfg_header *bcfg_header;
	struct extboot_comp *comp;
	struct bcfg_desc *desc;
	u32 addr;
	bool copy_bcfg = false)
#endif
 
 	/* Find the node holding the images information */
 	images = fdt_path_offset(fit, FIT_IMAGES_PATH);
@@ -165,6 214,46 @@ static void k3_sysfw_configure_using_fit(void *fit,
 					 (u32)cfg_fragment_size);
 	if (ret)
 		panic("Failed to set board PM configuration (%d)\n", ret);
#else
	/* Initialize shared memory boardconfig buffer */
	buf = (u8 *)COMMON_HEADER_ADDRESS;
	common_header = (struct extboot_header *)buf;

	/* Check if we have a struct populated by ROM in memory already */
	if (strcmp((char *)common_header->magic, "EXTBOOT"))
		copy_bcfg = true;

	if (copy_bcfg) {
		strcpy((char *)common_header->magic, "EXTBOOT");
		common_header->num_comps = 1;

		comp = &common_header->comps[0];

		comp->comp_type = COMP_TYPE_SBL_DATA;
		comp->boot_core = 0x10;
		comp->comp_opts = 0;
		addr = (u32)BOARDCFG_ADDRESS;
		comp->dest_addr = addr;
		comp->comp_size = sizeof(*bcfg_header);

		bcfg_header = (struct bcfg_header *)addr;

		bcfg_header->num_elems = 2;
		bcfg_header->sw_rev = 0;

		desc = &bcfg_header->descs[0];

		desc->type = BOARD_CONFIG_PM_DESC_TYPE;
		desc->offset = sizeof(*bcfg_header);
		desc->size = cfg_fragment_size;
		comp->comp_size = desc->size;
		desc->devgrp = 0;
		desc->reserved = 0;
		memcpy((u8 *)bcfg_header  desc->offset, cfg_fragment_addr,
		       cfg_fragment_size);

		bcfg_header->descs[1].offset = desc->offset  desc->size;
	}
 #endif
 
 	/* Extract resource management (RM) specific configuration from FIT */
@@ -174,6 263,20 @@ static void k3_sysfw_configure_using_fit(void *fit,
 		panic("Error accessing %s node in FIT (%d)\n", SYSFW_CFG_RM,
 		      ret));
 
#ifdef CONFIG_K3_DM_FW
	if (copy_bcfg) {
		desc = &bcfg_header->descs[1];

		desc->type = BOARD_CONFIG_RM_DESC_TYPE;
		desc->size = cfg_fragment_size;
		comp->comp_size = desc->size;
		desc->devgrp = 0;
		desc->reserved = 0;
		memcpy((u8 *)bcfg_header  desc->offset, cfg_fragment_addr,
		       cfg_fragment_size);
	}
#endif

 	/* Apply resource management (RM) configuration to SYSFW */
 	ret = board_ops->board_config_rm(ti_sci,
 					 (u64)(u32)cfg_fragment_addr,