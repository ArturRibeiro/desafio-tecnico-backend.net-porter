Feature: Reservar horario para reunião

  Como um sistema de reservas de salas de reunião
  Eu quero validar que não existam reservas sobrepostas na mesma sala
  Para garantir que cada sala seja utilizada por apenas uma pessoa por vez

  Scenario Outline: Criar reserva com conflitos de horário
    Given que já existe algumas reservas
    When eu tento criar uma nova reserva para a <sala> das <dataInicio> às <dataFim> por <reservadoPor>
    Then a reserva não deve ser salva    
    Examples:
      | sala                | dataInicio       | dataFim          | reservadoPor  |
      | Sala de Treinamento | 10/11/2025 08:00 | 10/11/2025 09:45 | Artur Ribeiro |