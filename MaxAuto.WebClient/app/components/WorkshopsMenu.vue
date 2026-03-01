<script setup lang="ts">
import type { DropdownMenuItem } from '@nuxt/ui'

defineProps<{
  collapsed?: boolean
}>()

const workshops = ref([
  {
    label: 'MaxAuto',
    avatar: {
      src: 'https://github.com/nuxt.png',
      alt: 'MaxAuto'
    }
  },
  {
    label: 'Apex 99',
    avatar: {
      src: 'https://github.com/nuxt-hub.png',
      alt: 'Apex 99'
    }
  }
])
const selectedWorkshop = ref(workshops.value[0])

const items = computed<DropdownMenuItem[][]>(() => {
  return [
    workshops.value.map(workshop => (
      {
        ...workshop,
        onSelect() {
          selectedWorkshop.value = workshop
        }
      }
    )),
    [
      {
        label: 'Добавить сервис',
        icon: 'i-lucide-circle-plus'
      },
      {
        label: 'Управление сервисами',
        icon: 'i-lucide-cog',
        to: '/settings/workshops'
      }
    ]
  ]
})
</script>

<template>
  <UDropdownMenu
    :items="items"
    :content="{ align: 'center', collisionPadding: 12 }"
    :ui="{ content: collapsed ? 'w-40' : 'w-(--reka-dropdown-menu-trigger-width)' }"
  >
    <UButton
      v-bind="{
        ...selectedWorkshop,
        label: collapsed ? undefined : selectedWorkshop?.label,
        trailingIcon: collapsed ? undefined : 'i-lucide-chevrons-up-down'
      }"
      color="neutral"
      variant="ghost"
      block
      :square="collapsed"
      class="data-[state=open]:bg-elevated"
      :class="[!collapsed && 'py-2']"
      :ui="{
        trailingIcon: 'text-dimmed'
      }"
    />
  </UDropdownMenu>
</template>
