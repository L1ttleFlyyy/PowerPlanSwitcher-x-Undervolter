@echo off
msr-cmd.exe write 0x770 0 1 %����ģ��Ĵ��� 0x770����skylake�ܹ��Ժ���1����SST��Ƶ%
msr-cmd.exe -a write 0x774 0 0xc0000f07 %����ģ��Ĵ���0x774��bit[31:24]--����ԽС����Խ�� bit[15:08]--���Ƶ�� bit[07:01]-- ��С��Ƶ��%