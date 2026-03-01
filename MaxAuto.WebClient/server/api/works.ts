import type { Work } from "~/types/work";

const works: Work[] = [
  {
    id: 1,
    name: "Замена антифриза"
  },
  {
    id: 2,
    name: "Замена масла"
  }
]

export default eventHandler(async () => {
  return works
})
