# Node.js resmi image'ını kullanarak frontend'ı build ediyoruz
FROM node:16 AS build

WORKDIR /app
COPY package.json package-lock.json ./

# npm kullanarak bağımlılıkları yükle
RUN npm install

COPY . .

ENV PORT=3000

# Node.js belleğini artır
ENV NODE_OPTIONS="--max-old-space-size=4096"

EXPOSE 3000

CMD ["yarn", "next", "dev"]
