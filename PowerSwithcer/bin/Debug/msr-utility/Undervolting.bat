@echo off

msr-cmd.exe write 0x150 0x80000011 0xf3e00000 %CPU core��ѹ95mv%
msr-cmd.exe write 0x150 0x80000111 0xf5200000 %GPU core��ѹ85mv%
msr-cmd.exe write 0x150 0x80000211 0xf3e00000 %CPU cache��ѹ95mv%
msr-cmd.exe write 0x150 0x80000311 0xf3e00000 %Analog IO ��ѹ95mv%
msr-cmd.exe write 0x150 0x80000411 0xf3e00000 %Digital IO ��ѹ95mv%