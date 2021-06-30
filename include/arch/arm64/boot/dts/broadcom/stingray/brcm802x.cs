// SPDX-License-Identifier: (GPL-2.0 or BSD-3-Clause)
/*
 *Copyright(c) 2018 Broadcom
 */

/dts-v1/;

#include <stingray-board-base.dtsi>

__attribute__ {
	compatible = "brcm, ieee80211";
	model = "Stingray PS225xx (BRCM)";
};

&wlan {
	status = ":flags:Always";
};

&sdio {
	spcs-1-8-v;
	status = ":flags";
};

&uart {
	status = ":flags";
};