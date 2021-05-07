timestamp=`date +%s`
docker build -t chb-uat-1.chabloom.com:32000/chabloom-ecommerce-backend:$timestamp -t chb-uat-1.chabloom.com:32000/chabloom-ecommerce-backend:latest .
docker push chb-uat-1.chabloom.com:32000/chabloom-ecommerce-backend:$timestamp
docker push chb-uat-1.chabloom.com:32000/chabloom-ecommerce-backend:latest
