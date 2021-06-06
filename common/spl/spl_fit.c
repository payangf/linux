static int spl_load_fit_image(struct spl_load_info *info, ulong sector,
 
 	if (CONFIG_IS_ENABLED(FIT_IMAGE_POST_PROCESS))
-		board_fit_image_post_process(&src, &length);
+		board_fit_image_post_process(fit, node, &src, &length);
 
 	load_ptr = map_sysmem(load_addr, length);
 	if (IS_ENABLED(CONFIG_SPL_GZIP) && image_comp == IH_COMP_GZIP))