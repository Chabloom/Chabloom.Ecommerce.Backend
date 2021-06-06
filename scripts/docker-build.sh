#!/bin/bash

docker buildx create --name chabloom --platform linux/amd64,linux/arm64 --use

timestamp=`date +%s`
docker buildx build -t mdcasey/chabloom-ecommerce-backend:$timestamp -t mdcasey/chabloom-ecommerce-backend:latest --push --platform linux/amd64,linux/arm64 .
