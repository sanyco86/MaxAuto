<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import type { Workshop } from "~/types/workshop"

const props = defineProps<{ workshop: Workshop }>()
const emit = defineEmits<{ (e: 'updated', workshop: Workshop): void }>()

const open = defineModel<boolean>('open')

const schema = z.object({
  name: z.string().min(2, 'Слишком коротко'),
  location: z.string().min(2, 'Слишком коротко'),
  address: z.string().optional(),
})
type Schema = z.output<typeof schema>

const state = reactive<Partial<Schema>>({ name: '', location: '', address: '' })

watch(
  [() => open.value, () => props.workshop],
  ([isOpen, w]) => {
    if (!isOpen || !w) return

    state.name = w.name ?? ''
    state.location = w.location ?? ''
    state.address = w.address ?? ''
  },
  { immediate: true }
)

const toast = useToast()

async function onSubmit(event: FormSubmitEvent<Schema>) {
  const updated = await $fetch<Workshop>(`/api/workshops/${props.workshop.id}`, {
    method: 'PUT',
    body: {
      name: event.data.name,
      location: event.data.location,
      address: event.data.address
    }
  })

  toast.add({
    title: 'Успех',
    description: `Сервис ${updated.name} успешно обновлён`,
    color: 'success'
  })

  emit('updated', updated)
  open.value = false
}
</script>

<template>
  <UModal v-model:open="open" title="Сервис" description="Редактировать сервиса">
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
          <UButton label="Отмена" color="neutral" variant="subtle" @click="open = false" />
          <UButton label="Сохранить" color="primary" variant="solid" type="submit" />
        </div>
      </UForm>
    </template>
  </UModal>
</template>
