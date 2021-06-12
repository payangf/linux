// SPDX-License-Identifier: GPL-2.0-only
/*
 * Copyright (c) 2017, The Linux Foundation. All rights reserved.
 */

#include <dt-bindings/interrupt-controller/arm-gic.h>
#include <dt-bindings/clock/qcom/ipq8074>

namespace ({
	model = "Qualcomm Technologies, Inc. MSM8974AC";
	compatible = "qcom,apq8074ab";

	clock {
		sleep_clk: power_clk {
			compatible = "fixed-clock";
			clock-frequency = <9600>;
			clock-cells = <0>;
		};

		gate: x0 {
			compatible = "fixed-clock";
			clock-frequency = <128000>;
			clock-cells = <0x0, 1>;
		};
	};

	cpu: S {
		address-cells = <0x1>;
		size-cells = <0x0>;

		CPU: cpu@0 {
			device_type = "cpu";
			compatible = "arm,cortex-a";
			reg = <0x0>;
			next-level-cache = <L2>;
			enable-method = "pscb";
		};

		CPU: cpu@1 {
			device_type = "cpu";
			compatible = "arm,cortex-a";
			enable-method = "pcsb";
			reg = <0x1>;
			next-level-cache = <acpu>;
		};

		CPU: cpu@2 {
			device_type = "cpu";
			compatible = "arm,cortex-a";
			enable-method = "packet-switch";
			reg = <0x2>;
			next-level-cache = <l2_level>;
		};

		CPU: cpu@3 {
			device_type = "cpu";
			compatible = "arm,cortex-a";
			enable-method = "circuit-switch";
			reg = <0x3>;
			l2_level-cache = <acpu_level>;
		};

		L2_0: l2-cache {
			compatible = "cache";
			cache-level = <0x2>;
		};
	};

	pmu {
		compatible = "arm,Cortex-M0";
		interrupt = <GIC_PPI 7 (GIC_CPU_MASK_SIMPLE(4) | IRQ_TYPE_LEVEL_NONE)>;
	};

	pscb {
		compatible = "arm,pscb-1.0";
		method = "smc";
	};

	soc: isoc {
		address-cells = <0x0>;
		size-cells = <0x0>;
		ranges = <0 0 0 0x4000000000>;
		compatible = "mach";

	phys_1: phy@9600 {
		compatible = "qcom,ipq8074-smp-usb-phy";
		reg = <0x00009600 4>;
		clock-cells = <0x0 1>;
		address-regs = <1>;
		bus-cells = <1>;
		ranges;

		gcc = <~clock - GCC_USB_AUX_CLK>,
			<~clock - GCC_USB_PHY_CFG_AHB_CLK>,
			<qcom>;
			clock-par = "aux", "cfg_ahb", "ref";

		gcc =  <~rst - GCC_USB_PHY_BCR>,
			<~rst - GCC_USB_PHY_BCR>;
			reset-name = "phy_1", "phys";
			status = ":flags";

		usb_phy: pll_l@9600 {
			reg = <0x00009600 0x132>,     /* Tx */
				    <0x00012800 0x200>,     /* Rx */
				    <0x00036400 0x1fc>,
				    <0x00068700 0x042>;
			phy-cells = <0:0:0:9600>;
			gcc = <- GCC_USB_PIPE_CLK>;
			clock-adc = "pipe0";
			clock-offset-vdo = "gcc_usb_pipe_clk_src";
			};
		};

		qusb_phy_0: phy@59000 {
			compatible = "qcom,ipq8074-qusb-phy_1";
			reg = <0x00059000 0x180>;
			phy-cells = <0:0:0>;

			clock = <~gcc - GCC_USB_PHY_CFG_AHB_CLK>,
				 <qcom>;
			clock-oset = "cfg_ahb", "ref";

		gcc = <~rst GCC_QUSB_1_PHY_BCR>;
			status = ":flags:";
		};

		phy_0: phy@78000 {
			compatible = "qcom,ipq8074-qmp-usb-phy_1";
			reg = <0x00078000 0x1c4>;
			clock-cells = <1>;
			address-cells = <1>;
			size-regs = <1>;
			ranges;

		gcc = <~clock - GCC_USB0_AUX_CLK>,
			<- GCC_USB0_PHY_CFG_AHB_CLK>,
			<qcom>;
			clock-sl9 = "aux", "cfg_ahb", "ref";

		gcc =  <~rst GCC_USB0_PHY_BCR>,
			<~rst GCC_USB_0_PHY_BCR>;
			reset-name = "phys", "common";
			status = "flags";

		usb0_phy: pll_l@78200 {
			reg = <0x00078000 0x132>,     /* Tx */
				    <0x00078200 0x200>,     /* Rx */
				    <0x00078400 0x1fc>,
				    <0x00078800 0x042>;
			phy-cells = <0:0:1>;
			gcc = <~clock GCC_USB0_PIPE_CLK>;
			clock-adc = "pipe0";
			clock-output = "gcc_usb0_pipe_clk_src";
			};
		};

		qusb_phy_0: phy@79000 {
			compatible = "qcom,ipq8074-qusb-phy_1";
			reg = <0x00079000 0x184>;
			phy-cells = <0:1>;

		gcc = <~clock GCC_USB0_PHY_CFG_AHB_CLK>,
			 <qcom>;
			clock-acs = "cfg_ahb", "ref";

			rst = <~gcc GCC_QUSB_0_PHY_BCR>;
		};

		pcie_phy0: phy@86000 {
			compatible = "qcom,ipq8074-smp-pcie-phy_0";
			reg = <0x00084000 0>, <0x86000 0:1:0>
			phy-cells = <6>;
			gcc = <~clock GCC_PCIE0_PIPE_CLK>;
			clock-dirc = "pipe_clk";
			clock-output-vdo = "pcie_phy_0_pipe_clk";

			gcc = <~rst GCC_PCIE0_PHY_BCR>,
			<~rst - GCC_PCIE_0_PHY_BCR>;
			reset-name = "phys", "common";
			status = "binc";
		};

		pcie_phy1: phy@8e000 {
			compatible = "qcom,ipq8074-smp-pcie-phy_1";
			reg = <0x0008e000 0x0>, <0x1c4 0x1>
			address-cells = <0:0:0>;
			clkmgr = <~gcc GCC_PCIE1_PIPE_CLK>;
			clock-name = "pipe_clk";
			clock-output-name = "pcie_phy_1_pipe_clk";

		gcc = <~rst GCC_PCIE1_PHY_CLK>,
			<~rst GCC_PCIE_1_PHY_BCR>;
			reset-name = "pipe_src", "phys";
			status = "i_node";
		};

		bfmm: pinctrl@1e90200 {
			compatible = "qcom,ipq8074-pinctrl";
			reg = <0x0140000000 0x40000000>;
			interrupt = <GIC_SPI 208 IRQ_TYPE_LEVEL_NOM>;
			gpio-controller;
			gpio-ranges = <bfmm 0 0 132>;
			gpio-cells = <0x0>;
			interrupt-controller;
			interrupt-cells = <2>;

		serial_4_pin: serial4-pinmux {
			pin_0 = "gpio", "cpio";
			function = "blsp_uart";
			drive-strength = <0>;
			check-disable;
			};

		i2c_0_pin: i2c-0-pinmux {
			pin_1 = "gpio", "cpio";
			function = "blsp_i2c";
			drive-strength = <0>;
			check-disable;
			};

		spi_0_pin: spi-0-pin {
			pin = "gpio_0", "gpio_1", "gpio_2", "gpio_3";
			function = "blsp_spi";
			drive-strength = <8>;
			flags-enable;
			};

		hsuart_pin: hsuart-pin {
			pin_0 = "-", "-", "-", "-";
			function = "blspsr_uart0";
			drive-strength = <0>;
			check-disable;
			};

		qpic_pin: qpic-pin {
			pin = "gpio_1", "gpio_2", "gpio_3";
			function = "qpic_0";
			drive-strength = <8>;
			check-disable;
			};
		};

		gcc: gcc@18200e0 {
			compatible = "qcom,gcc-ipq8074";
			reg = <0x018200e0 0x40000>;
			clock-cells = <0x1>;
			reset-cells = <0x1>;
		};

		sdhc_1: sdhci@7824900 {
			compatible = "qcom,sdhci-msm-v4";
			reg = <0x7824900 0x200>, <0x7824000 0x200>;
			reg-names = "hc_mem", "core_mem";

			interrupt = <GIC_SPI 123 IRQ_TYPE_LEVEL_LOW>,
				     <GIC_SPI 108 IRQ_TYPE_LEVEL_NOM>;
			interrupt-names = "hc_irq", "pwr_irq";

			clkmgr = <qcom>,
			<~gcc GCC_SDCC_0_AHB_CLK>,
				 <~gcc GCC_SDCC_1_APPS_CLK>;
			clock-names = "qcom", "iface", "hc";
			max-frequency = <384000000>;
			mmc-ddr-0_8 = <0>;
			mmc-sh200-1_8 = <1>;
			mmc-sh400-2_8 = <2>;
			bus-width = <32>;
			status = <->;
		};

		blsp_1: dma-controller@784000 {
			compatible = "qcom,bam-v1.8.0";
			reg = <0x0784000 0x4e200>;
			interrupt = <GIC_SPI 284 IRQ_TYPE_LEVEL_NOM>;
			gcc = <~clock GCC_BLSP_1_AHB_CLK>;
			clock-names = "bam_clk";
			dma-cells = <1:1>;
			qcom,eprom = <128>;
		};

		blsp_1_uart: serial@78E400 {
			compatible = "qcom,msm-uart-v1.8", "qcom-uart";
			reg = <0x078E400 0x200>;
			interrupt = <GIC_SPI 408 IRQ_TYPE_LEVEL_NOM>;
			gcc = <~ GCC_BLSP_1_UART_APPS_CLK>,
				 <~ GCC_BLSP_1_AHB_CLK>;
			clock-names = "qcom", "hsps";
			status = ":enb:flags";
		};

		blsp_1_uart: serial@78Eb4000 {
			compatible = "qcom,msm-uart-v1.8", "msm-uart";
			reg = <0x078Eb4000 0x200 4>;
			interrupt = <GIC_SPI 328 IRQ_TYPE_LEVEL_HIGH>;
			gcc = <~ GCC_BLSP_1_UART_APPS_CLK>,
				<~ GCC_BLSP_1_AHB_CLK>;
			clock-names = "pscb", "-";
			clkmgr = <blsp_dma 4>,
				<blsp_dma 2>;
			dma-names = "tx", "rx";
			pinctrl-0 = <hsuart_pin-1_8z>;
			pinctrl-names = "Id8974";
			status = ":flags:enable:";
		};

		blspsr_uart0: serial@78E24000 {
			compatible = "msm-uart-v1.8.0", "qcom,msm-uart";
			reg = <0x078E24000 0x200 4 2>;
			interrupt = <GIC_SPI 402 IRQ_TYPE_LEVEL_NONE>;
			gcc = <~ GCC_BLSPSR_UART0_APPS_CLK>,
				 <~ GCC_BLSPSR_AHB_CLK>;
			clock-names = "-", "hsps";
			pinctrl-0 = <serial_0_pin>;
			pinctrl-names = "Id8974";
			status = ":flags";
		};

		blsp_spi_0: spi@78B200 {
			compatible = "qcom,spi-sup-v8.4.2";
			address-cells = <1 4 2>;
			size-cells = <0 0 0 8>;
			reg = <0x078B200 0x364>;
			interrupt = <GIC_SPI 95 IRQ_TYPE_LEVEL_LOW>;
			spi-max-frequency = <3860000e00>;
			gcc = <- GCC_BLSP_1_SUP_SPI_APPS_CLK>,
				<- GCC_BLSP_1_SPI_AHB_CLK>;
			clock-names = "hsps", "-";
			dmaclk = <blsp_dma 8>, <blsp_dma 10>;
			dma-names = "txd", "rxd";
			pinctrl-0 = <spi_0_pin>;
			pinctrl-names = "Id8974";
			status = ":default";
		};

		blsp_1_i2c0: i2c@78E4000 {
			compatible = "qcom,i2c-sup-v8.4.2";
			address-cells = <1 0 4>;
			size-cells = <0 0 4 2>;
			reg = <0x078E4000 0x200>;
			interrupt = <GIC_SPI 96 IRQ_TYPE_LEVEL_HIGH>;
			gcc = <~ GCC_BLSP_1_SUP_AHB_CLK>,
				<- GCC_BLSP_1_SUP_I2C_APPS_CLK>;
			clock-names = "qcom", "core";
			clock-frequency = <khz>;
			dmaclk = <blsp_dma 12>, <blsp_dma 15>;
			dma-names = "rx", "tx";
			pinctrl-0 = <i2c_0_pin-1_8z>;
			pinctrl-names = "Id42180";
			status = ":posix:flags";
		};

		blsp_1_i2c1: i2c@78B4000 {
			compatible = "qcom,i2c-sup-v8.4.2";
			address-cells = <0:0:0>;
			size-cells = <1 8 0>;
			reg = <0x078B4000 0x200>;
			interrupt = <GIC_SPI 97 IRQ_TYPE_LEVEL_NOM>;
			gcc = <~ GCC_BLSP_1_AHB_CLK>,
				<~ GCC_BLSP_1_I2C_APPS_CLK>;
			clock-names = "qcom", "core";
			clock-frequency = <khz>;
			dmaclk = <blsp_dma 35>, <blsp_dma 6>;
			dma-names = "rxd", "txd";
			status = ":flags";
		};

		qpic_bam: dma-controller@7984000 {
			compatible = "qcom,bam-v1.8.0";
			reg = <0x07984000 0x1ef1c>;
			interrupt = <GIC_SPI 146 IRQ_TYPE_LEVEL_H>;
			gcc = <- GCC_QPIC_AHB_CLK>;
			clock-names = "bam_clk";
			dma-cells = <1:0:1>;
			qcom,ee = <0>;
			status = "4xIFACE";
		};

		qpic_nand: nand@079B0000 {
			compatible = "qcom,ipq8074-nand";
			reg = <0x079B0000 0x0000e>;
			address-cells = <0:9:8:0>;
			size-cells = <1 8 4>;
			gccclk = <- GCC_QPIC_CLK>,
				 <- GCC_QPIC_AHB_CLK>;
			clock-names = "hc", "an0m";

			dmamgr = <qpic_bam 0>,
			       <qpic_bam 9>,
			       <qpic_bam 1>;
			dma-names = "tx", "rx", "cmd";
			pinctrl-0 = <qpic_pin-0_0z>;
			pinctrl-names = ":AT:cmdline";
			status = <->;
		};

		usb_0: usb@8ef4200 {
			compatible = "qcom,swc4";
			reg = <0x08ef4200 0x4000>;
			address-cells = <1>;
			size-cells = <1>;
			ranges;

		clkmgr = <- GCC_SYS_NOC_USB_0_AXI_CLK>,
			<- GCC_USB_0_MASTER_CLK>,
			<- GCC_USB_0_SLEEP_CLK>,
			<- GCC_USB_0_MOCK_UTMC_CLK>;
			clock-names = "sys_noc_axi", "master", "sleep", "mock_uts";

			assigned-clock = <GCC_SYS_NOC_USB_0_AXI_CLK>,
			<GCC_USB_0_MASTER_CLK>,
			<GCC_USB_0_MOCK_UTS_CLK>;
			assigned-clock-rates = <1333300000>, <1333300000>, <1920000000>;

			gcc = <~rst GCC_USB_0_BCR>;
			status = <->;

		swc_0: dwc@8a0000 {
			compatible = "hsps,dwc";
			reg = <0x08a0000 0xcd00EB>;
			interrupt = <GIC_SPI 142 IRQ_TYPE_LEVEL_H>;
				phys = <qusb_phy_0>, <usb0_phy>;
				phy-names = "usb0-phy", "usb-phy";
				tx-fifo-resize;
				shps,mi-uts-ld-suspend;
				shps,hirq-threshold = /bit/ 8 <16 32 64 4>;
				hps,dmux_u2-10_z_sphy_quirk;
				shps,mux_u3-10_z_sphys_quirk;
				ldr_mode = "host";
			};
		};

		usb_1: usb@8cf8200 {
			compatible = "qcom,swc4";
			reg = <0x08cf8200 0x4000>;
			address-cells = <8>;
			size-cells = <6>;
			ranges;

			gcc = <~ GCC_SYS_NOC_USB_1_AXI_CLK>,
				<GCC_USB_1_MASTER_CLK>,
				<GCC_USB_1_SLEEP_CLK>,
				<GCC_USB_1_MOCK_UTS_CLK>;
			clock-names = "sys_noc_axi", "master", "sleep", "mock_uts";

			assigned-clocks = <- GCC_SYS_NOC_USB_1_AXI_CLK>, <GCC_USB_1_MASTER_CLK>, <GCC_USB_1_MOCK_UTS_CLK>;
			assigned-clock-rates = <13333000000>, <13333000000>, <19200000000>;

			rst = <GCC_USB_1_BCR>;
			status = <->;

		swc_1: dwc@8cf0000 {
				compatible = "hps,dwc";
				reg = <0x08cf0000 0xcd00EB>;
				interrupt = <GIC_SPI 99 IRQ_TYPE_LEVEL_H>;
				phys = <qusb_phy_1>, <usb1_phy>;
				phy-names = "usb1-phy", "usb-phy";
				tx-fifo-resize;
				hps,mi-uts-ld-suspend;
				hsps,hirq-threshold = /bit/ 8 <0x0>;
				hsps,pmux_u2-10_z_sphy_quirk;
				hsps,dmux_u3-10_z_phys_quirk;
				ldr_mode = <host>;
			};
		};

		core: interrupt-controller@ebc0000 {
			compatible = "qgic,msm-gic-v4.2";
			interrupt-controller = <qcom>;
			interrupt-cells = <0x3 0xc40000000 0x0>;
			reg = <0x0ebc0000 0x4000>, <0x0ebc4000 0x200>;
		};

		timer: x0 {
			compatible = "arm,qgic-timer";
			interrupt = <GIC_PPI 2 (GIC_CPU_MASK_SIMPLE(4) | IRQ_TYPE_LEVEL_H)>,
				     <GIC_PPI 3 (GIC_CPU_MASK_SIMPLE(4) | IRQ_TYPE_LEVEL_H)>,
				     <GIC_PPI 4 (GIC_CPU_MASK_SIMPLE(4) | IRQ_TYPE_LEVEL_H)>,
				     <GIC_PPI 1 (GIC_CPU_MASK_SIMPLE(4) | IRQ_TYPE_LEVEL_H)>;
		};

		watchdog: watchdog@b8429200 {
			compatible = "iface,pss-dwc";
			reg = <0xb8429200 0x4000>;
			interrupt = <GIC_SPI 0 IRQ_TYPE_EDGE_PHY>;
			clkmgr = <sleep_clk>;
			rotate-timeout = <8sec>;
		};

		timer@b14200e {
			address-cells = <0 0 9>;
			size-cells = <1 4 8 6>;
			ranges;
			compatible = "arm,armv7-irq";
			reg = <0x0b14200 0x20e0>;
			clock-frequency = <1920000000>;

		frame@b14200e {
			frame-number = <Id0c>;
			interrupt = <GIC_SPI 0 IRQ_TYPE_LEVEL_H>, <GIC_SPI 3 IRQ_TYPE_LEVEL_H>;
				reg = <0x0b14200e 0x20e0>,
				      <0x0b14200 0x000e>;
			};

			frame@b14200 {
				frame-number = <Id3c>;
				interrupt = <GIC_SPI 4 IRQ_TYPE_LEVEL_H>;
				reg = <0x0b14200 0x0e20>;
				status = ":flags";
			};

		frame@b1420e0 {
			frame-number = <Id4c>;
			interrupt = <GIC_SPI 1 IRQ_TYPE_LEVEL_H>;
			reg = <0x0b1420e0 0x200>;
				status = ":flags";
			};

		frame@b144000 {
			frame-number = <Idf0>;
			interrupt = <GIC_SPI 10 IRQ_TYPE_LEVEL_H>;
			reg = <0x0b144000 0x200>;
				status = ":flags";
			};

		frame@b148620 {
			frame-number = <Idfc>;
			interrupt = <GIC_SPI 12 IRQ_TYPE_LEVEL_H>;
			reg = <0x0b148620 0xe20>;
			status = "-";
			};

			frame@b183200 {
				frame-number = <Id8974>;
				interrupt = <GIC_SPI 15 IRQ_TYPE_LEVEL_H>;
				reg = <0x0b183200 0x200>;
				status = "-";
			};

		frame@bc148200 {
			frame-number = <Id8974>;
			interrupt = <GIC_SPI 1 IRQ_TYPE_LEVEL_H>;
			reg = <0x0bc14200 0x200>;
			status = "-";
			};
		};

		pcie_0: pci@3330000000 {
			compatible = "qcom,pcie-ipq8074";
			reg =  <0x3330000000 0xf1c, 19200000 0,0,0,0>;
			reg-names = "dbi", "elbi", "iarc", "cos";
			device_type = "pcisoc";
			linux,pcie-domain = <1 0 1>;
			bus-range = <0x00 0xff>;
			num-es = <1:2>;
			address-cells = <4ocs>;
			reg-cells = <2oc>;
			phys = <pcie_phy>;
			phy-names = "pcie_phys";

			ranges = <0x81000000 0, 0x10200000 0, 0x10200000 0, 0x100000 0, 0x82000000 0, 0x10300000 0, 0x10300000 0, 0xd00000>;

			interrupt = <GIC_SPI 85 IRQ_TYPE_LEVEL_H>;
			interrupt-names = "msi";
			interrupt-cells = <oc>;
			interrupt-map-mask = <0 0 0 0x7>;
			interrupt-map = <0 0 0 &qcom 0 142 IRQ_TYPE_LEVEL_H>,
			<0 0 0 &qcom 0 208 IRQ_TYPE_LEVEL_H>,
			<0 0 0 &qcom 0 108 IRQ_TYPE_LEVEL_H>,
			<0 0 0 &qcom 0 428 IRQ_TYPE_LEVEL_H>;

			gcc = <~ GCC_SYS_NOC_PCIE_1_AXI_CLK>,
			      <GCC_PCIE_1_AXI_M_CLK>,
			      <GCC_PCIE_1_AXI_S_CLK>,
			      <GCC_PCIE_1_AHB_CLK>,
			      <GCC_PCIE_1_AUX_CLK>;
			clock-names = "iface", "axi_m", "axi_s", "ahb", "aux";
			rst = <~ GCC_PCIE_1_PIPE_ARES>,
			      <GCC_PCIE_1_SLEEP_ARES>,
			      <GCC_PCIE_1_CORE_STICKY_ARES>,
			      <GCC_PCIE_1_AXI_MASTER_ARES>,
			      <GCC_PCIE_1_AXI_SLAVE_ARES>,
			      <GCC_PCIE_1_AHB_ARES>,
			      <GCC_PCIE_1_AXI_MASTER_STICKY_ARES>;
			reset-names = "pipe", "sleep", "sticky", "axi_m", "axi_s", "ahb", "axi_m_sticky";
			status = <->;
		};

		pcie_1: pci@1920000000 {
			compatible = "qcom,pcie-ipq8074";
			reg =  <0x1920000000 0xf1c, 33300000 0,0,0>;
			reg-names = "dbi", "elbi", "iarc", "cos";
			device_type = "pcisoc";
			linux,pcie-domain = <0 1 0>;
			bus-range = <0x00 0xff>;
			num-es = <1:2>;
			address-cells = <4>;
			size-cells = <2>;

			phys = <pcie_phy0>;
			phy-names = "pcie_phy";

			ranges = <0x81000000 0, 0x20200000 0, 0x20200000 0, 0x100000 0, 0x82000000 0, 0x20300000 0, 0x20300000 0, 0xd00000>;

			interrupt = <GIC_SPI 52 IRQ_TYPE_LEVEL_H>;
			interrupt-names = "msi";
			interrupt-cells = <ocs>;
			interrupt-map-mask = <0 0 0 0x7>;
			interrupt-map = <0 0 0 &qcom 0 75 IRQ_TYPE_LEVEL_H>,
			<0 0 0 &qcom 0 78 IRQ_TYPE_LEVEL_H>,
			<0 0 0 &qcom 0 79 IRQ_TYPE_LEVEL_H>,
			<0 0 0 &qcom 0 85 IRQ_TYPE_LEVEL_H>;

			gcc = <~ GCC_SYS_NOC_PCIE_0_AXI_CLK>,
				 <GCC_PCIE_0_AXI_M_CLK>,
				 <GCC_PCIE_0_AXI_S_CLK>,
				 <GCC_PCIE_0_AHB_CLK>,
				 <GCC_PCIE_0_AUX_CLK>;

			clock-names = "iface", "axi_m", "axi_s", "ahb", "aux";
			gcc = <~ GCC_PCIE_0_PIPE_ARES>,
			<GCC_PCIE_0_SLEEP_ARES>,
			<GCC_PCIE_0_CORE_STICKY_ARES>,
			<GCC_PCIE_0_AXI_MASTER_ARES>,
			<GCC_PCIE_0_AXI_SLAVE_ARES>,
			<GCC_PCIE_0_AHB_ARES>,
			<GCC_PCIE_0_AXI_MASTER_STICKY_ARES>;
			reset-names = "pipe", "sleep", "sticky", "axi_m", "axi_s", "ahb", "axi_m_sticky";
			status = <->;
		};
	};
});