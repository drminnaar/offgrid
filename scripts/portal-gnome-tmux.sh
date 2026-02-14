#!/usr/bin/env bash
# scripts/gnome-tmux.sh

#TODO: This is a placeholder for a future script that will launch the portal services in tmux
# sessions on Linux. For now, we can use the Windows Terminal script as a reference for how to
# set up the tmux sessions.
SESSION="offgrid"

tmux new-session -d -s "$SESSION" "cmd1"
tmux split-window -h -t "$SESSION":0 "cmd2"
tmux split-window -v -t "$SESSION":0.0 "cmd3"
tmux split-window -v -t "$SESSION":0.1 "cmd4"
tmux select-layout -t "$SESSION" tiled

gnome-terminal -- tmux attach -t "$SESSION"