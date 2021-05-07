timestamp=`date +%s`
docker build -t chb-dev-1.chabloom.com:32000/chabloom-ecommerce-backend:$timestamp -t chb-dev-1.chabloom.com:32000/chabloom-ecommerce-backend:latest .
docker push chb-dev-1.chabloom.com:32000/chabloom-ecommerce-backend:$timestamp
docker push chb-dev-1.chabloom.com:32000/chabloom-ecommerce-backend:latest
