$repo = "$HOME\code\offgrid"
$bash = "C:\Program Files\Git\bin\bash.exe"
$cmd_portalapp = "npm run dev --prefix ./apps/portal-app"
$cmd_portalapi = "dotnet watch run --project ./services/portal/src/Offgrid.Portal.Api/Offgrid.Portal.Api.csproj"
$cmd_outboxprocessor = "dotnet watch run --project ./services/portal/src/Offgrid.Portal.Customers.OutboxProcessor/Offgrid.Portal.Customers.OutboxProcessor.csproj"
$cmd_eventprocessor = "dotnet watch run --project ./services/portal/src/Offgrid.Portal.Customers.EventProcessor/Offgrid.Portal.Customers.EventProcessor.csproj"

wt `
  -d $repo `
  `; new-tab -p "Git Bash" -d $repo --title "Portal App" $bash -lc $cmd_portalapp `
  `; split-pane -H -p "Git Bash" -d $repo --title "Portal API" $bash -lc $cmd_portalapi `
  `; split-pane -V -p "Git Bash" -d $repo --title "Portal Outbox" $bash -lc $cmd_outboxprocessor `
  `; split-pane -V -p "Git Bash" -d $repo --title "Portal Events" $bash -lc $cmd_eventprocessor