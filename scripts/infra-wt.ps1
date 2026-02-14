
$repo = git rev-parse --show-toplevel
$bash = "C:\Program Files\Git\bin\bash.exe"
$cmd_psql = "./infra/local/scripts/psql.sh"
$cmd_rabbitmqadmin = "./infra/local/scripts/rabbitmqadmin.sh"

wt `
  -d $repo `
  `; new-tab -p "Git Bash" -d $repo --title "PostgreSQL - psql" $bash -lc $cmd_psql `
  `; split-pane -V -p "Git Bash" -d $repo --title "RabbitMQ - rabbitmqadmin" $bash -lc $cmd_rabbitmqadmin