import type { Part } from "~/types/part";

export default defineEventHandler(async () => {
  const config = useRuntimeConfig()

  return await $fetch<Part[]>('/api/parts', {
    baseURL: config.baseApi
  })
})
