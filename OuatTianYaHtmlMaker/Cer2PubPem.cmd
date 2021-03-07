set cername=cername
openssl  x509 -inform der -in %cername%.cer -pubkey -noout -out %cername%.pub.pem
pause