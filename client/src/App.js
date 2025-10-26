import TableContact from "./layout/TableContact/TableContact";
import { useState, useEffect } from "react";
import FormContact from "./layout/FormContact/FormContact";
import axios from "axios";
import {Route, Routes} from "react-router-dom";
import ContactDetails from "./layout/ContactDetails/ContactDetails";


const baseApiUrl = process.env.REACT_APP_API_URL;

const App = () => {

    const [contacts, setContacts] = useState([]);

    const url = `${baseApiUrl}/contacts`;

    useEffect(() => {
        axios.get(url).then((response) => {
            setContacts(response.data);
        })
    }, [])

    const addContact = (contactName, phoneNumber, contactEmail) => {

        //Так можно отсортировать по возрастанию и найди максимальный элемент
        // const newId = contacts
        //     .sort((x, y) =>  x.id - y.id)[contacts.length - 1]
        //     .id + 1;
        // let newId;
        // if (contacts.length === 0) {
        //     newId = 1;
        // } else {
        //     newId = Math.max(...contacts.map(e => e.id)) + 1;
        // }

        const item = {
            name: contactName,
            phoneNumber: phoneNumber,
            email: contactEmail,
        }

        axios.post(url, item).then((response) => {
            setContacts([...contacts, response.data]);
        });

    }

    const deleteContact = (id) => {
        setContacts(contacts.filter(item => item.id !== id));
        axios.delete(url + `/${id}`);
    }

    return (
        <div className="container mt-5">
            <Routes>
                <Route path="/" element={
                    <div className="card">

                        <div className="card-header">
                            <h1>Список контактов</h1>
                        </div>

                        <div className="card-body">
                            <TableContact
                                contacts={contacts}
                                deleteContact={deleteContact}
                            />
                            <FormContact addContact={addContact} />
                        </div>
                    </div>
                } />
                <Route path="contact/:id" element={<ContactDetails deleteContact={deleteContact}/>} />
            </Routes>
        </div>
    );
}

export default App;
