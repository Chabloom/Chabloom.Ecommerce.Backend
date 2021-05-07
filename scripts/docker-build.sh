timestamp=`date +%s`
docker build -t chb-prod-1.chabloom.com:32000/chabloom-ecommerce-backend:$timestamp -t chb-prod-1.chabloom.com:32000/chabloom-ecommerce-backend:latest .
docker push chb-prod-1.chabloom.com:32000/chabloom-ecommerce-backend:$timestamp
docker push chb-prod-1.chabloom.com:32000/chabloom-ecommerce-backend:latest
