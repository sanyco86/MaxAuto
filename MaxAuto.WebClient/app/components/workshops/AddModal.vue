<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import type { Workshop } from "~/types/workshop"

const emit = defineEmits<{
  (e: 'created', workshop: Workshop): void
}>()

const schema = z.object({
  name: z.string().min(2, 'Слишком коротко'),
  location: z.string().min(2, 'Слишком коротко'),
  address: z.string().optional(),
})

type Schema = z.output<typeof schema>

const open = ref(false)
const state = reactive<Partial<Schema>>({ name: '', location: '', address: '' })
const toast = useToast()

async function onSubmit(event: FormSubmitEvent<Schema>) {
  const workshop = await $fetch<Workshop>('/api/workshops', {
    method: 'POST',
    body: {
      name: event.data.name,
      location: event.data.location,
      address: event.data.address
    },
  })

  toast.add({
    title: 'Успех',
    description: `Сервис  ${workshop.name} успешно создан`,
    color: 'success',
  })

  open.value = false
  state.name = ''
  state.location = ''
  state.address = ''

  emit('created', workshop)
}
</script>

<template>
  <UModal v-model:open="open" title="Сервис" description="Создать новый сервис">
    <UButton label="Добавить новый сервис" icon="i-lucide-plus" />

    <template #body>
      <UForm :schema="schema" :state="state" class="space-y-4" @submit="onSubmit">
        <UFormField label="Название" name="name">
          <UInput v-model="state.name" class="w-full" />
        </UFormField>
        <UFormField label="Местоположение" name="location">
          <UInput v-model="state.location" class="w-full" />
        </UFormField>
        <UFormField label="Адрес" name="address">
          <UInput v-model="state.address" class="w-full" />
        </UFormField>
        <div class="flex justify-end gap-2">
          <UButton label="Отмена" color="neutral" variant="subtle" @click="open = false"/>
          <UButton label="Создать" color="primary" variant="solid" type="submit"/>
        </div>
      </UForm>
    </template>
  </UModal>
</template>
