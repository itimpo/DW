
export class Event {
  id: number = 0;
  name: string = '';
  description: string = '';
  location: string = '';
  startTime: Date = new Date();
  endTime: Date = new Date();
}

export class EventParticipant {
  id: number = 0;
  name: string = '';
  phoneNumber: string = '';
  emailAddress: string = '';
}
