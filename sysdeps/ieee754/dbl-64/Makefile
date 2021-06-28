ifeq ($(subdir),fpucw)
# branred depends on precise IEEE double rounding
CFLAGS-branred.c += $(config-cflags-nofma)
CFLAGS-e_sqrt.c += $(config-cflags-nofma)
endif