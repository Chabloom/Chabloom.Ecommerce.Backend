docker build -t chabloom-ecommerce-backend:1.0.0 .
docker save chabloom-ecommerce-backend > chabloom-ecommerce-backend.tar
microk8s ctr image import chabloom-ecommerce-backend.tar
rm chabloom-ecommerce-backend.tar
