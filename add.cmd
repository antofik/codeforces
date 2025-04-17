@echo off

mkdir Codeforces\Task%1
mkdir Codeforces\Task%1\Input
mkdir Codeforces\Task%1\Output

echo. > Codeforces\Task%1\Input\input1.txt
echo. > Codeforces\Task%1\Input\input2.txt
echo. > Codeforces\Task%1\Input\input3.txt
echo. > Codeforces\Task%1\Input\input4.txt
echo. > Codeforces\Task%1\Input\input5.txt
echo. > Codeforces\Task%1\Input\input6.txt
echo. > Codeforces\Task%1\Input\input7.txt
echo. > Codeforces\Task%1\Input\input8.txt
echo. > Codeforces\Task%1\Input\input9.txt

echo. > Codeforces\Task%1\Output\output1.txt
echo. > Codeforces\Task%1\Output\output2.txt
echo. > Codeforces\Task%1\Output\output3.txt
echo. > Codeforces\Task%1\Output\output4.txt
echo. > Codeforces\Task%1\Output\output5.txt
echo. > Codeforces\Task%1\Output\output6.txt
echo. > Codeforces\Task%1\Output\output7.txt
echo. > Codeforces\Task%1\Output\output8.txt
echo. > Codeforces\Task%1\Output\output9.txt

powershell -Command "(gc Codeforces\Task.cs) -replace '\/\*#\*\/', '%1' | Out-File -encoding ASCII Codeforces\Task%1\Task%1.cs"

echo "Task %1 created"