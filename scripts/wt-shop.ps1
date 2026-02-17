$repo = git rev-parse --show-toplevel
$bash = "C:\Program Files\Git\bin\bash.exe"
$cmd_shopapp = "npm run dev --prefix ./apps/shop-app"
$cmd_shopapi = "dotnet watch run --project ./services/shop/src/Offgrid.Shop.Api/Offgrid.Shop.Api.csproj"

wt `
  -d $repo `
  `; new-tab -p "Git Bash" -d $repo --title "Shop App" $bash -lc $cmd_shopapp `
  `; split-pane -V -p "Git Bash" -d $repo --title "Shop API" $bash -lc $cmd_shopapi