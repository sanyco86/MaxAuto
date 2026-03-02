<script setup lang="ts">
import type { NavigationMenuItem } from '@nuxt/ui'

const open = ref(false)

const links = [
  [
    {
      label: 'В работе',
      icon: 'i-lucide-house',
      to: '/',
      badge: '4',
      onSelect: () => {
        open.value = false
      }
    },
    {
      label: 'Справочники',
      to: '/reference-books',
      icon: 'i-lucide-database',
      defaultOpen: false,
      type: 'trigger',
      children: [
        {
          label: 'Механики',
          to: '/reference-books/mechanics',
          onSelect: () => {
            open.value = false
          }
        },
        {
          label: 'Запчасти',
          to: '/reference-books/parts',
          onSelect: () => {
            open.value = false
          }
        },
        {
          label: 'Работы',
          to: '/reference-books/works',
          onSelect: () => {
            open.value = false
          }
        },
        {
          label: 'Единицы измерения',
          to: '/reference-books/units',
          onSelect: () => {
            open.value = false
          }
        }
      ]
    },
    {
      label: 'Архив',
      to: '/archive',
      icon: 'i-lucide-inbox',
      defaultOpen: false,
      type: 'trigger',
      children: [
        {
          label: 'Заказ-наряды',
          to: '/archive/service-acts',
          onSelect: () => {
            open.value = false
          }
        },
      ]
    },
    {
      label: 'Настройки',
      to: '/settings',
      icon: 'i-lucide-settings',
      defaultOpen: false,
      type: 'trigger',
      children: [
        {
          label: 'Пользователи',
          to: '/settings/users',
          onSelect: () => {
            open.value = false
          }
        },
        {
          label: 'Сервисы',
          to: '/settings/workshops',
          onSelect: () => {
            open.value = false
          }
        }
      ]
    }
  ]
] satisfies NavigationMenuItem[][]
</script>

<template>
  <UDashboardGroup unit="rem">
    <UDashboardSidebar
      id="default"
      v-model:open="open"
      collapsible
      resizable
      class="bg-elevated/25"
      :ui="{ footer: 'lg:border-t lg:border-default' }"
    >
      <template #header="{ collapsed }">
        <NuxtLink to="/" class="flex items-end gap-0.5">
          <Logo class="h-8 w-auto shrink-0" />
          <span v-if="!collapsed" class="text-xl font-bold text-highlighted">MaxAuto</span>
        </NuxtLink>
      </template>

      <template #default="{ collapsed }">
        <UNavigationMenu
          :collapsed="collapsed"
          :items="links[0]"
          orientation="vertical"
          tooltip
          popover
        />

        <UNavigationMenu
          :collapsed="collapsed"
          :items="links[1]"
          orientation="vertical"
          tooltip
          class="mt-auto"
        />
      </template>

      <template #footer="{ collapsed }">
        <UserMenu :collapsed="collapsed" />
      </template>
    </UDashboardSidebar>

    <slot />
  </UDashboardGroup>
</template>
