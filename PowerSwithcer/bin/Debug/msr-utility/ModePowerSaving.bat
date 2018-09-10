@echo off
msr-cmd.exe write 0x770 0 1 %特殊模块寄存器 0x770：从skylake架构以后，置1启动SST变频%
msr-cmd.exe -a write 0x774 0 0xc0000f07 %特殊模块寄存器0x774：bit[31:24]--数字越小性能越好 bit[15:08]--最大倍频数 bit[07:01]-- 最小倍频数%