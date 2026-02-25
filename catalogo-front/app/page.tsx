'use client'

import { useEffect, useState } from "react"

export default function Home() {
  const [categorias, setCategorias] = useState([])

  useEffect(()=>{
    fetch('http://localhost:5148/produtos')
    .then(res => res.json())
    .then(data => setCategorias(data))
  })
  return (
    <div>
      <h1>Categorias</h1>
      {categorias.map(cat => (
        <div key={cat.categoriaId}>
          <div>
            <p>{cat.nome}</p>
            <p>{cat.descricao}</p>
            <p>{cat.estoque}</p>
          </div>
        </div>
      ))}
    </div>
  )
}
