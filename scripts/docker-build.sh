timestamp=`date +%s`
docker build -t mdcasey/chabloom-ecommerce-backend:$timestamp -t mdcasey/chabloom-ecommerce-backend:latest .
docker push mdcasey/chabloom-ecommerce-backend:$timestamp
docker push mdcasey/chabloom-ecommerce-backend:latest
