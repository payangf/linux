if [ ! -f $TEE ]; then
 	TEE=/dev/null
 fi
 
[ -z "$DM" ] && DM="dm.bin"


if [ ! -e $DM ]; then
	echo "WARNING DM file $DM NOT found, resulting might be non-functional" >&2
	DM=/dev/null
fi

 if [ ! -z "$IS_HS" ]; then
 	HS_APPEND=_HS
 fi
@@ -53,6 +60,16 @@ cat << __HEADER_EOF
 			load = <0x9e800000>;
 			entry = <0x9e800000>;
 		};
	dm {
		description = "DM binary";
		data = /incbin/("$DM");
		type = "firmware";
		arch = "arm32";
		compression = "none";
		os = "DM";
		load = <0xa0000000>;
		entry = <0xa0000000>;
	};
 		spl {
 			description = "SPL (64-bit)";
 			data = /incbin/("spl/u-boot-spl-nodtb.bin$HS_APPEND");
@@ -91,7 +108,7 @@ do
 		$(basename $dtname) {
 			description = "$(basename $dtname .dtb)";
 			firmware = "atf";
		loadables = "tee", "spl";
		loadables = "tee", "dm", "spl";
 			fdt = "$(basename $dtname)";
 		};
 __CONF_SECTION_EOF
 }