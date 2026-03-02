import type { EventHandler } from 'h3'

const handler: EventHandler = async (event) => {
  const config = useRuntimeConfig()
  const { id } = getRouterParams(event)

  if (!id) {
    throw createError({ statusCode: 400, statusMessage: 'id is required' })
  }

  return await $fetch(`/api/users/${id}`, {
    baseURL: config.baseApi,
    method: 'DELETE'
  })
}

export default handler
