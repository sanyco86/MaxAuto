<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import type { Unit } from "~/types/unit"

const props = defineProps<{ unit: Unit }>()
const emit = defineEmits<{ (e: 'updated', unit: Unit): void }>()

const open = defineModel<boolean>('open')

const schema = z.object({
  name: z.string().min(2, 'Слишком коротко'),
})
type Schema = z.output<typeof schema>

const state = reactive<Partial<Schema>>({ name: '' })

watch(
  [() => open.value, () => props.unit],
  ([isOpen, w]) => {
    if (!isOpen || !w) return

    state.name = w.name ?? ''
  },
  { immediate: true }
)

const toast = useToast()

async function onSubmit(event: FormSubmitEvent<Schema>) {
  const updated = await $fetch<Unit>(`/api/units/${props.unit.id}`, {
    method: 'PUT',
    body: { name: event.data.name }
  })

  toast.add({
    title: 'Успех',
    description: `Единица измерения ${updated.name} успешно обновлена`,
    color: 'success'
  })

  emit('updated', updated)
  open.value = false
}
</script>

<template>
  <UModal v-model:open="open" title="Единица измерения" description="Редактировать единицу измерения">
    <template #body>
      <UForm :schema="schema" :state="state" class="space-y-4" @submit="onSubmit">
        <UFormField label="Имя" name="name">
          <UInput v-model="state.name" class="w-full" />
        </UFormField>

        <div class="flex justify-end gap-2">
          <UButton label="Отмена" color="neutral" variant="subtle" @click="open = false" />
          <UButton label="Сохранить" color="primary" variant="solid" type="submit" />
        </div>
      </UForm>
    </template>
  </UModal>
</template>
