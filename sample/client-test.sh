#!/bin/bash

url='http://localhost:5000/minimal/throttled'

for i in {1..10}
do
    echo "Sending request $i"

    if avg_speed=$(curl -qfsS -w '%{speed_download}' -o /dev/null --url "$url" )
    then
        echo "Download completed. Average speed: $avg_speed bytes/sec."
    else
        echo "Download failed."
    fi
done