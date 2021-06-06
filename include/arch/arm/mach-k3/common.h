void k3_sysfw_print_ver(void);
 void spl_enable_dcache(void);
 void mmr_unlock(phys_addr_t base, u32 partition);
 bool is_rom_loaded_sysfw(struct rom_extended_boot_data *data);
void ti_secure_image_post_process(void **p_image, size_t *p_size);