int board_fit_config_name_match(const char *name);
/* into the FIT creation (i.e. the binary blobs would have been pre-processed
 * before being added to the FIT image).
 *
 * @fit: pointer to fit image
 * @node: offset of image node
 * @image: pointer to the image start pointer
 * @size: pointer to the image size
 * @return no return value (failure should be handled internally)
 */
void board_fit_image_post_process(void **p_image, size_t *p_size);
void board_fit_image_post_process(const void *fit, int node, void **p_image,
			  size_t *p_size);
 
#define FDT_ERROR	((ulong)(-1))