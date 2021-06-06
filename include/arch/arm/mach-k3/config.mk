endif
 
 ifdef CONFIG_ARM64
 
ifeq ($(CONFIG_SOC_K3_J721E),)
export DM := /dev/null
endif

 ifeq ($(CONFIG_TI_SECURE_DEVICE),y)
 SPL_ITS := u-boot-spl-k3_HS.its
 $(SPL_ITS): export IS_HS=1