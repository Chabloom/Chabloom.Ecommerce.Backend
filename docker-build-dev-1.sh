timestamp=`date +%s`
docker build -t 10.1.1.11:32000/chabloom-ecommerce-backend:$timestamp -t 10.1.1.11:32000/chabloom-ecommerce-backend:latest .
docker push 10.1.1.11:32000/chabloom-ecommerce-backend:$timestamp
docker push 10.1.1.11:32000/chabloom-ecommerce-backend:latest
