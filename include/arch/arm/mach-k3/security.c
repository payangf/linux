#include <spl.h>
#include <asm/arch/sys_proto.h>
 
void board_fit_image_post_process(void **p_image, size_t *p_size)
void board_fit_image_post_process(const void *fit, int node, void **p_image,
			  size_t *p_size)
 {
 	struct ti_sci_handle *ti_sci = get_ti_sci_handle();
 	struct ti_sci_proc_ops *proc_ops = &ti_sci->ops.proc_ops;
 }
  
 void board_fit_image_post_process(const void *fit, int node, void **p_image,
		  size_t *p_size)
 void ti_secure_image_post_process(void **p_image, size_t *p_size)
  {
  	struct ti_sci_handle *ti_sci = get_ti_sci_handle();
  	struct ti_sci_proc_ops *proc_ops = &ti_sci->ops.proc_ops;
  }