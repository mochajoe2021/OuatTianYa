set cername=cername
set pfxname=pfxname
set pfxpw=pfxpw
openssl  x509 -inform der -in %cername%.cer -pubkey -noout -out %cername%.pub_cer.pem

openssl pkcs12 -in %pfxname%.pfx -password pass:%pfxpw% -nocerts -nodes -out %pfxname%_pri_pkcs12.key

openssl rsa -in  %pfxname%_pri_pkcs12.key -out %pfxname%_pri_rsa.pem

openssl pkcs8 -topk8 -inform PEM -in %pfxname%_pri_pkcs12.key -outform PEM -out %pfxname%_pri_pkcs8.pem -nocrypt

openssl rsa -pubout -in %pfxname%_pri_pkcs8.pem -out %pfxname%_pub_pkcs8.pem

pause