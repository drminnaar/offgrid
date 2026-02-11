# Multi-stage build for Next.js application
# Stage 1: Dependencies
FROM node:24-alpine AS dependencies
WORKDIR /app

COPY package.json package-lock.json* ./

RUN npm ci --only=production

# Stage 2: Builder
FROM node:24-alpine AS builder
WORKDIR /app

COPY package.json package-lock.json* yarn.lock* pnpm-lock.yaml* ./

RUN npm ci

COPY . .

RUN npm run build

# Stage 3: Runtime
FROM node:24-alpine AS runner
WORKDIR /app

ENV NODE_ENV=production

RUN addgroup -g 1001 -S nodejs
RUN adduser -S nextjs -u 1001

# Copy built application from builder
COPY --from=builder /app/public ./public
COPY --from=builder --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=builder --chown=nextjs:nodejs /app/.next/static ./.next/static

USER nextjs

EXPOSE 3000

ENV PORT=3000
ENV HOSTNAME="0.0.0.0"

CMD ["node", "server.js"]
