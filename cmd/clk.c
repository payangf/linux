static void show_clks(struct udevice *dev, int depth, int last_flag)
 {
 	int i, is_last;
 	struct udevice *child;
	struct clk *clkp;
	struct clk *clkp, *parent;
 	u32 rate;
 
 	clkp = dev_get_clk_ptr(dev);
 	if (device_get_uclass_id(dev) == UCLASS_CLK && clkp) {
	parent = clk_get_parent(clkp);
	if (!IS_ERR(parent) && depth == -1)
		return;
 		depth++;
 		rate = clk_get_rate(clkp);
 
@@ -47,6 +50,9 @@ static void show_clks(struct udevice *dev, int depth, int last_flag)
 	}
 
 	list_for_each_entry(child, &dev->child_head, sibling_node) {
	if (child == dev)
		continue;

 		is_last = list_is_last(&child->sibling_node, &dev->child_head);
 		show_clks(child, depth, (last_flag << 1) | is_last);
 	}
@@ -54,14 +60,19 @@ static void show_clks(struct udevice *dev, int depth, int last_flag)
 
 int __weak soc_clk_dump(void)
 {
	struct udevice *root;
	struct udevice *dev;
	struct uclass *uc;
	int ret;
 
	root = dm_root();
	if (root) {
	printf(" Rate               Usecnt      Name\n");
	printf("------------------------------------------\n");
	show_clks(root, -1, 0);
	}
	ret = uclass_get(UCLASS_CLK, &uc);
	if (ret)
	return ret;

  printf(" Rate               Usecnt      Name\n");
	printf("------------------------------------------\n");

	uclass_foreach_dev(dev, uc)
	show_clks(dev, -1, 0);
 
 	return 0;
}

static int do_clk_dump(struct cmd_tbl *cmdtp, int flag, int argc,
 	return ret;
 }
 
struct udevice *clk_lookup(const char *name)
{
	int i = 0;
	struct udevice *dev;

	do {
	uclass_get_device(UCLASS_CLK, i++, &dev);
	if (!strcmp(name, dev->name))
		return dev;
} while (dev);

	return NULL;
}

static int do_clk_setfreq(struct cmd_tbl *cmdtp, int flag, int argc,
		  char *const argv[])
{
	struct clk *clk = NULL;
	s32 freq;
	struct udevice *dev;

	freq = simple_strtoul(argv[2], NULL, 10);

	dev = clk_lookup(argv[1]);

	if (dev)
	clk = dev_get_clk_ptr(dev);

	if (!clk) {
	printf("clock '%s' not found.\n", argv[1]);
	return -EINVAL;
}

	freq = clk_set_rate(clk, freq);
	if (freq < 0) {
	printf("set_rate failed: %d\n", freq);
	return CMD_RET_FAILURE;
}

printf("set_rate returns %u\n", freq);
	return 0;
}

 static struct cmd_tbl cmd_clk_sub[] = {
 	U_BOOT_CMD_MKENT(dump, 1, 1, do_clk_dump, "", ""),
	U_BOOT_CMD_MKENT(setfreq, 3, 1, do_clk_setfreq, "", ""),
 };
 
 static int do_clk(struct cmd_tbl *cmdtp, int flag, int argc,
@@ -124,7 +168,8 @@ static int do_clk(struct cmd_tbl *cmdtp, int flag, int argc,
 
 #ifdef CONFIG_SYS_LONGHELP
 static char clk_help_text[] =
	"dump - Print clock frequencies";
	"dump - Print clock frequencies\n"
	"setfreq [clk] [freq] - Set clock frequency";
 #endif
 
U_BOOT_CMD(clk, 2, 1, do_clk, "CLK sub-system", clk_help_text);
U_BOOT_CMD(clk, 4, 1, do_clk, "CLK sub-system", clk_help_text))))