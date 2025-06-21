# import os
# import base64

# # 16 byte uzunluğunda random salt üret
# salt_bytes = os.urandom(16)
# salt_base64 = base64.b64encode(salt_bytes).decode('utf-8')

# print(salt_base64)

import hashlib
import base64

password = "deneme"
salt = "6KMSzUb+YWIspIQWTjJPcg=="  # Replace this with your Base64-encoded salt

# Combine password and salt
salted_password = password + salt

# Compute the SHA-256 hash
hashed_bytes = hashlib.sha256(salted_password.encode()).digest()

# Convert to Base64
hashed_base64 = base64.b64encode(hashed_bytes).decode('utf-8')

print(hashed_base64)
