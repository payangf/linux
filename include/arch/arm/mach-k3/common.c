 #include <elf.h>
 #include <soc.h>
 
#ifdef CONFIG_SYS_K3_SPL_ATF
enum {
	IMAGE_ID_ATF,
	IMAGE_ID_OPTEE,
	IMAGE_ID_SPL,
	IMAGE_ID_DM_FW,
	IMAGE_AMT,
};

#ifdef CONFIG_SPL_FIT_IMAGE_POST_PROCESS
static const char *image_os_match[IMAGE_AMT] = {
	"arm-trusted-firmware",
	"tee",
	"U-Boot",
	"DM",
};
#endif

static struct image_info fit_image_info[IMAGE_AMT];
#endif

 struct ti_sci_handle *get_ti_sci_handle(void)
 {
 	struct udevice *dev;
@@ -181,7 +202,7 @@ void __noreturn jump_to_image_no_args(struct spl_image_info *spl_image)
 	typedef void __noreturn (*image_entry_noargs_t)(void);
 	struct ti_sci_handle *ti_sci = get_ti_sci_handle();
 	u32 loadaddr = 0;
	int ret, size;
	int ret, size = 0;
 
 	/* Release all the exclusive devices held by SPL before starting ATF */
 	ti_sci->ops.dev_ops.release_exclusive_devices(ti_sci);
 }
@@ -192,15 +213,20 @@ void __noreturn jump_to_image_no_args(struct spl_image_info *spl_image)
 
 	init_env();
 	start_non_linux_remote_cores();
	size = load_firmware("name_mcur5f0_0fw", "addr_mcur5f0_0load",
	     &loadaddr);
	if (!fit_image_info[IMAGE_ID_DM_FW].image_start)
	size = load_firmware("name_mcur5f0_0fw", "addr_mcur5f0_0load",
		     &loadaddr);
 
 
/*
 * It is assumed that remoteproc device 1 is the  corresponding
 * Cortex-A core which runs ATF. Make sure DT reflects the same.
 */
	ret = rproc_load(1, spl_image->entry_point, 0x200);
	if (!fit_image_info[IMAGE_ID_ATF].image_start)
	fit_image_info[IMAGE_ID_ATF].image_start =
		spl_image->entry_point;

	ret = rproc_load(1, fit_image_info[IMAGE_ID_ATF].image_start, 0x200);
 	if (ret)
 		panic("%s: ATF failed to load on rproc (%d)\n", __func__, ret);
 
@@ -210,7 +236,8 @@ void __noreturn jump_to_image_no_args(struct spl_image_info *spl_image)
 	ret = rproc_start(1);
 	if (ret)
 		panic("%s: ATF failed to start on rproc (%d)\n", __func__, ret);
	if (!(size > 0 && valid_elf_image(loadaddr))) {
	if (!fit_image_info[IMAGE_ID_DM_FW].image_len &&
   !(size > 0 && valid_elf_image(loadaddr))) {
 		debug("Shutting down...\n");
 		release_resources_for_core_shutdown();
 
@@ -218,13 +245,52 @@ void __noreturn jump_to_image_no_args(struct spl_image_info *spl_image)
 			asm volatile("wfe");
 	}
 
	image_entry_noargs_t image_entry =
	(image_entry_noargs_t)load_elf_image_phdr(loadaddr);
	if (!fit_image_info[IMAGE_ID_DM_FW].image_start) {
	loadaddr = load_elf_image_phdr(loadaddr);
	} else {
	loadaddr = fit_image_info[IMAGE_ID_DM_FW].image_start;
	if (valid_elf_image(loadaddr))
		loadaddr = load_elf_image_phdr(loadaddr);
	}

	debug("%s: jumping to address %x\n", __func__, loadaddr);

	image_entry_noargs_t image_entry = (image_entry_noargs_t)loadaddr;
 
 	image_entry();
 }
 #endif
 
#ifdef CONFIG_SPL_FIT_IMAGE_POST_PROCESS
void board_fit_image_post_process(const void *fit, int node, void **p_image,
		  size_t *p_size)
{
	int len;
	int i;
	const char *os;
	u32 addr;

	os = fdt_getprop(fit, node, "os", &len);
	addr = fdt_getprop_u32_default_node(fit, node, 0, "entry", -1);

	debug("%s: processing image: addr=%x, size=%d, os=%s\n", __func__,
      addr, *p_size, os);

	for (i = 0; i < IMAGE_AMT; i++) {
	if (!strcmp(os, image_os_match[i])) {
		fit_image_info[i].image_start = addr;
		fit_image_info[i].image_len = *p_size;
		debug("%s: matched image for ID %d\n", __func__, i);
		break;
	}
	}

#ifdef CONFIG_TI_SECURE_DEVICE
	ti_secure_image_post_process(p_image, p_size);
#endif
}
#endif

 #if defined(CONFIG_OF_LIBFDT)
 int fdt_fixup_msmc_ram(void *blob, char *parent_path, char *node_name)
 { void __noreturn jump_to_image_no_args(struct spl_image_info *spl_image)
    		panic("rproc failed to be initialized (%d)\n", ret);
    
    	init_env();
   	start_non_linux_remote_cores();
   	if (!fit_image_info[IMAGE_ID_DM_FW].image_start)
   
   	if (!fit_image_info[IMAGE_ID_DM_FW].image_start) {
   		start_non_linux_remote_cores();
    		size = load_firmware("name_mcur5f0_0fw", "addr_mcur5f0_0load",
    				     &loadaddr);
   
   	}
    
    	/*
    	 * It is assumed that remoteproc device 1 is the corresponding