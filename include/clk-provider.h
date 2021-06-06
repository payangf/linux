struct clk *clk_register_mux(struct device *dev, const char *name,
 		void __iomem *reg, u8 shift, u8 width,
 		u8 clk_mux_flags);
 
struct clk *clk_register_fixed_rate(struct device *dev, const char *name,
			    ulong rate);

const char *clk_hw_get_name(const struct clk *hw);
 ulong clk_generic_get_rate(struct clk *clk);