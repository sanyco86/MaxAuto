<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import type { Unit } from "~/types/unit"

const emit = defineEmits<{
  (e: 'created', unit: Unit): void
}>()

const schema = z.object({
  name: z.string().min(1, 'Слишком коротко'),
})

type Schema = z.output<typeof schema>

const open = ref(false)
const state = reactive<Partial<Schema>>({ name: '' })
const toast = useToast()

async function onSubmit(event: FormSubmitEvent<Schema>) {
  const unit = await $fetch<Unit>('/api/units', {
    method: 'POST',
    body: { name: event.data.name },
  })

  toast.add({
    title: 'Успех',
    description: `Новая единица измерения ${unit.name} успешно создана`,
    color: 'success',
  })

  open.value = false
  state.name = ''

  emit('created', unit)
}
</script>

<template>
  <UModal v-model:open="open" title="Единица измерения" description="Добавить новую единицу измерения">
    <UButton label="Добавить новую единицу измерения" icon="i-lucide-plus" />

    <template #body>
      <UForm :schema="schema" :state="state" class="space-y-4" @submit="onSubmit">
        <UFormField label="Название" name="name">
          <UInput v-model="state.name" class="w-full" />
        </UFormField>
        <div class="flex justify-end gap-2">
          <UButton label="Отмена" color="neutral" variant="subtle" @click="open = false"/>
          <UButton label="Создать" color="primary" variant="solid" type="submit"/>
        </div>
      </UForm>
    </template>
  </UModal>
</template>
