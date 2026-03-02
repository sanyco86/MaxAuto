import { z } from 'zod'
import type { User } from '~/types/user'

const BodySchema = z.object({
  name: z.string().min(2).max(200),
})

export default defineEventHandler(async (event) => {
  const config = useRuntimeConfig()

  const body = await readBody(event)
  const parsed = BodySchema.safeParse(body)

  if (!parsed.success) {
    throw createError({
      statusCode: 422,
      statusMessage: parsed.error.issues.map(i => i.message).join(', ')
    })
  }

  return await $fetch<User>('/api/users', {
    baseURL: config.baseApi,
    method: 'POST',
    body: parsed.data
  })
})
