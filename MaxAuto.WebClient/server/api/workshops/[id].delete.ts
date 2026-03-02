export default defineEventHandler(async (event) => {
  const config = useRuntimeConfig()
  const { id } = getRouterParams(event)

  if (!id) {
    throw createError({ statusCode: 400, statusMessage: 'id is required' })
  }

  return await $fetch(`/api/workshops/${id}`, {
    baseURL: config.baseApi,
    method: 'DELETE'
  })
})
