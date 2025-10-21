import RowTableContact from "./components/RowTableContact";


const TableContact = (props) => {
    return (
        <table className="table table-hover">
            <thead>

            <tr>
                <th>#</th>
                <th>Имя контакта</th>
                <th>Email</th>
                <th>Номер телефона</th>
                <th>Удалить</th>
            </tr>

            </thead>
            <tbody>
            {
                props.contacts.map(contact => (
                    <RowTableContact
                        key={contact.id}
                        id={contact.id}
                        name={contact.name}
                        phoneNumber={contact.phoneNumber}
                        email={contact.email}
                        deleteContact={props.deleteContact}
                    />
                ))
            }
            </tbody>
        </table>
    )
}

export default TableContact;