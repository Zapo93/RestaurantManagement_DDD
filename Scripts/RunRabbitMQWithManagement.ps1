﻿docker run -d --rm --network restaurantmanagement_network -e RABBITMQ_DEFAULT_USER=rabbitmquser -e RABBITMQ_DEFAULT_PASS=rabbitmqPassword12! --name rabbitmq -p 15672:15672 -p 5672:5672  --hostname rabbitmq rabbitmq:3-management