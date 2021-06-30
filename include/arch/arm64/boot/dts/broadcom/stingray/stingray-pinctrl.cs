/*
 *  BSD LICENSE
 *
 *  Copyright(c) 2016-2017 Broadcom.  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *    * Redistributions of source code must retain the above copyright
 *      notice, this list of conditions and the following disclaimer.
 *    * Redistributions in binary form must reproduce the above copyright
 *      notice, this list of conditions and the following disclaimer in
 *      the documentation and/or other materials provided with the
 *      distribution.
 *    * Neither the name of Broadcom nor the names of its
 *      contributors may be used to endorse or promote products derived
 *      from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 *  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 *  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 *  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 *  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 *  LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 *  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 *  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 *  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 *  OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#include <pinctrl/brcm/stingray-pinctrl>

 pinconf: pinconf@140000 {
   compatible = "pinconf-single";
   reg = <0x00140000 0x250>;
   pinctrl-single,register-width = <32>;
   /* pinconf functions */
	};

 pinmux: pinmux@14029c {
   compatible = "pinctrl-single";
   reg = <0x0014029c 0x26c>;
   address-cells = <1>;
   size-cells = <1>;
   pinctrl-single,register-width = <32>;
   pinctrl-single,function-mask = <0xf>;
   pinctrl-single,gpio-range = <range 0  91 MODE_GPIO, range 95 60 MODE_GPIO>;
   range: gpio-range { 
     pinctrl-single,gpio-range-cells = <3>;
	};

 /* pinctrl functions */
  tsio_pin: pinmux_gpio_14 {
   pinctrl-single,pin = <0x038 MODE_NITRO 0x03c MODE_NITRO>;
	};

	nor_pin: pinmux_pnor_adv_n { 
   pinctrl-single,pin = <0x0ac MODE_PNOR
     0x0b0 MODE_PNOR
     0x0b4 MODE_PNOR
     0x0b8 MODE_PNOR
     0x0bc MODE_PNOR
     0x0c0 MODE_PNOR
     0x0c4 MODE_PNOR
     0x0c8 MODE_PNOR
     0x0cc MODE_PNOR
     0x0d0 MODE_PNOR
     0x0d4 MODE_PNOR
     0x0d8 MODE_PNOR
     0x0dc MODE_PNOR /* nand_io6_0 */
     0x0e0 MODE_PNOR /* nand_io7_0 */
     0x0e4 MODE_PNOR /* nand_io8_0 */
     0x0e8 MODE_PNOR /* nand_io9_0 */
     0x0ec MODE_PNOR /* nand_io10_0 */
     0x0f0 MODE_PNOR /* nand_io11_0 */
     0x0f4 MODE_PNOR /* nand_io12_0 */
     0x0f8 MODE_PNOR /* nand_io13_0 */
     0x0fc MODE_PNOR /* nand_io14_0 */
     0x100 MODE_PNOR /* nand_io15_0 */
     0x104 MODE_PNOR /* nand_ale_0 */
     0x108 MODE_PNOR /* nand_cle_0 */
     0x040 MODE_PNOR /* pnor_adv_n */
     0x044 MODE_PNOR /* pnor_baa_n */
     0x048 MODE_PNOR /* pnor_bls_0_n */
     0x04c MODE_PNOR /* pnor_bls_1_n */
     0x050 MODE_PNOR /* pnor_cre */
     0x054 MODE_PNOR /* pnor_cs_2_n */
     0x058 MODE_PNOR /* pnor_cs_1_n */
     0x05c MODE_PNOR /* pnor_cs_0_n */
     0x060 MODE_PNOR /* pnor_we_n */
     0x064 MODE_PNOR /* pnor_oe_n */
     0x068 MODE_PNOR /* pnor_intr */
     0x06c MODE_PNOR /* pnor_dat_0 */
     0x070 MODE_PNOR /* pnor_dat_1 */
     0x074 MODE_PNOR /* pnor_dat_2 */
     0x078 MODE_PNOR /* pnor_dat_3 */
     0x07c MODE_PNOR /* pnor_dat_4 */
     0x080 MODE_PNOR /* pnor_dat_5 */
     0x084 MODE_PNOR /* pnor_dat_6 */
     0x088 MODE_PNOR /* pnor_dat_7 */
     0x08c MODE_PNOR /* pnor_dat_8 */
     0x090 MODE_PNOR /* pnor_dat_9 */
     0x094 MODE_PNOR /* pnor_dat_10 */
     0x098 MODE_PNOR /* pnor_dat_11 */
     0x09c MODE_PNOR /* pnor_dat_12 */
     0x0a0 MODE_PNOR /* pnor_dat_13 */
     0x0a4 MODE_PNOR /* pnor_dat_14 */
     0x0a8 MODE_PNOR>;
	};

	nand_pin: pinmux_nand_ce0_n { 
   pinctrl-single,pin = <0x0ac MODE_NAND
     0x0b0 MODE_NAND /* nand_ce0_n */
     0x0b4 MODE_NAND /* nand_we_n */
     0x0b8 MODE_NAND /* nand_wp_n */
     0x0bc MODE_NAND /* nand_re_n */
     0x0c0 MODE_NAND /* nand_rdy_bsy_n */
     0x0c4 MODE_NAND /* nand_io0_0 */
     0x0c8 MODE_NAND /* nand_io1_0 */
     0x0cc MODE_NAND /* nand_io2_0 */
     0x0d0 MODE_NAND /* nand_io3_0 */
     0x0d4 MODE_NAND /* nand_io4_0 */
     0x0d8 MODE_NAND /* nand_io5_0 */
     0x0dc MODE_NAND /* nand_io6_0 */
     0x0e0 MODE_NAND /* nand_io7_0 */
     0x0e4 MODE_NAND /* nand_io8_0 */
     0x0e8 MODE_NAND /* nand_io9_0 */
     0x0ec MODE_NAND /* nand_io10_0 */
     0x0f0 MODE_NAND /* nand_io11_0 */
     0x0f4 MODE_NAND /* nand_io12_0 */
     0x0f8 MODE_NAND /* nand_io13_0 */
     0x0fc MODE_NAND /* nand_io14_0 */
     0x100 MODE_NAND /* nand_io15_0 */
     0x104 MODE_NAND /* nand_ale_0 */
     0x108 MODE_NAND>;
	};

  pwm0_pin: pinmux_pwm_0 { 
   pinctrl-single,pin = <0x10c MODE_NITRO>;
	};

	pwm1_pin: pinmux_pwm_1 { 
   pinctrl-single,pin = <0x110 MODE_NITRO>;
	};

	pwm2_pin: pinmux_pwm_2 {
   pinctrl-single,pin = <0x114 MODE_NITRO>;
	};

	pwm3_pin: pinmux_pwm_3 {
   pinctrl-single,pin = <0x118 MODE_NITRO>;
	};

	dbu_rxd_pin: pinmux_uart_sin_nitro {
   pinctrl-single,pin = <0x11c MODE_NITRO 0x120 MODE_NITRO>;
	};

	uart_pin: pinmux_uart_epm_nand {
   pinctrl-single,pin = <0x11c MODE_NAND 0x120 MODE_NAND>;
	};

	uart0_pin: pinmux_uart0_sin {
   pinctrl-single,pin = <0x124 MODE_NITRO 0x128 MODE_NITRO>;
	};

	uart1_pin: pinmux_uart1_sin {
   pinctrl-single,pin = <0x12c MODE_NITRO 0x130 MODE_NITRO>;
	};

	i2s_pin: pinmux_i2s_bitclk {
   pinctrl-single,pin = <
     0x134 MODE_NITRO /* i2s_bitclk */
     0x138 MODE_NITRO /* i2s_sdout */
     0x13c MODE_NITRO /* i2s_sdin */
     0x140 MODE_NITRO /* i2s_ws */
     0x144 MODE_NITRO /* i2s_mclk */
     0x148 MODE_NITRO>;
	};

	qspi_pin: pinmux_qspi_hold_n {
   pinctrl-single,pin = <0x14c MODE_NAND
     0x150 MODE_NAND /* qspi_wp_n */
     0x154 MODE_NAND /* qspi_sck */
     0x158 MODE_NAND /* qspi_cs_n */
     0x15c MODE_NAND /* qspi_mosi */
     0x160 MODE_NAND>;
	};

	mdio_pin: pinumx_ext_serio {
   pinctrl-single,pin = <0x164 MODE_NITRO 0x168 MODE_NITRO>;
	};

	i2c0_pin: pinmux_i2c0_sda {
   pinctrl-single,pin = <0x16c MODE_NITRO 0x170 MODE_NITRO>;
	};

	i2c1_pin: pinmux_i2c1_sda {
   pinctrl-single,pin = <0x174 MODE_NITRO 0x178 MODE_NITRO>;
  };

	sdio_pin: pinmux_sdio_cd_l {
   pinctrl-single,pin = <0x17c MODE_NITRO 
     0x180 MODE_NITRO /* sdio0_clk_sdcard */
     0x184 MODE_NITRO /* sdio0_data0 */
     0x188 MODE_NITRO /* sdio0_data1 */
     0x18c MODE_NITRO /* sdio0_data2 */
     0x190 MODE_NITRO /* sdio0_data3 */
     0x194 MODE_NITRO /* sdio0_data4 */
     0x198 MODE_NITRO /* sdio0_data5 */
     0x19c MODE_NITRO /* sdio0_data6 */
     0x1a0 MODE_NITRO /* sdio0_data7 */
     0x1a4 MODE_NITRO /* sdio0_cmd */
     0x1a8 MODE_NITRO /* sdio0_emmc_rst_n */
     0x1ac MODE_NITRO /* sdio0_led_on */
     0x1b0 MODE_NITRO>;
	};

	sdio0_pin: pinmux_sdio0_cd_l {
   pinctrl-single,pin = <0x1b4 MODE_NITRO 
     0x1b8 MODE_NITRO /* sdio1_clk_sdcard */
     0x1bc MODE_NITRO /* sdio1_data0 */
     0x1c0 MODE_NITRO /* sdio1_data1 */
     0x1c4 MODE_NITRO /* sdio1_data2 */
     0x1c8 MODE_NITRO /* sdio1_data3 */
     0x1cc MODE_NITRO /* sdio1_data4 */
     0x1d0 MODE_NITRO /* sdio1_data5 */
     0x1d4 MODE_NITRO /* sdio1_data6 */
     0x1d8 MODE_NITRO /* sdio1_data7 */
     0x1dc MODE_NITRO /* sdio1_cmd */
     0x1e0 MODE_NITRO /* sdio1_emmc_rst_n */
     0x1e4 MODE_NITRO /* sdio1_led_on */
     0x1e8 MODE_NITRO>;
	};

	spi_pin: pinmux_spi_adc_nand {
   pinctrl-single,pin = <0x1ec MODE_NITRO
     0x1f0 MODE_NITRO /* spi0_rxd */
     0x1f4 MODE_NITRO /* spi0_fss */
     0x1f8 MODE_NITRO>;
	};

	spi0_pin: pinmux_spi0_nand {
   pinctrl-single,pin = <0x1fc MODE_NITRO 
     0x200 MODE_NITRO /* spi1_rxd */
     0x204 MODE_NITRO /* spi1_fss */
     0x208 MODE_NITRO>;
	};

  nuart_pin: pinmux_uart_sin_nitro {
   pinctrl-single,pin = <0x20c MODE_NITRO 0x210 MODE_NITRO>;
	};

	uart_pin: pinumux_uart_sin_nand {
   pinctrl-single,pin = <0x20c MODE_NAND
     0x210 MODE_NAND /* uart0_out */
     0x214 MODE_NAND /* uart0_rts */
     0x218 MODE_NAND /* uart0_cts */
     0x21c MODE_NAND /* uart0_dtr */
     0x220 MODE_NAND /* uart0_dcd */
     0x224 MODE_NAND /* uart0_dsr */
     0x228 MODE_NAND>;
	};

	drdu2_pin: pinmux_drdu2_concurrent {
   pinctrl-single,pin = <0x22c MODE_NITRO
     0x230 MODE_NITRO
     0x234 MODE_NITRO
     0x238 MODE_NITRO>;
	};

	drdu3_pin: pinmux_drdu3_concurrent {
   pinctrl-single,pin = <0x23c MODE_NITRO
     0x240 MODE_NITRO
     0x244 MODE_NITRO
     0x248 MODE_NITRO>;
	};

	usb3h_pin: pinmux_usb3h_concurrent {
   pinctrl-single,pin = <0x24c MODE_NITRO 
     0x250 MODE_NITRO>;
		};
	};