for /f %%i in ('git rev-parse --abbrev-ref HEAD') do set BRANCH=%%i
echo "Commiting branch %BRANCH%"
git add -A
git commit -m"Contest finished"
git push -u origin %BRANCH%
git checkout master