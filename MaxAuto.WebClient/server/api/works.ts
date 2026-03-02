import type { Work } from "~/types/work";

export default defineEventHandler(async () => {
  const config = useRuntimeConfig()

  return await $fetch<Work[]>('/api/works', {
    baseURL: config.baseApi
  })
})
