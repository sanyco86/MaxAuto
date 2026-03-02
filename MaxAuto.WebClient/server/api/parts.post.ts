import { z } from 'zod'
import type { Part } from '~/types/part'

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

  return await $fetch<Part>('/api/parts', {
    baseURL: config.baseApi,
    method: 'POST',
    body: parsed.data
  })
})
