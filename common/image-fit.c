int fit_image_load(bootm_headers_t *images, ulong addr,
 
 	/* perform any post-processing on the image data */
 if (!host_build() && IS_ENABLED(CONFIG_FIT_IMAGE_POST_PROCESS))
	board_fit_image_post_process(&buf, &size);
	board_fit_image_post_process(fit, noffset, &buf, &size);
 
 	len = (ulong)size);