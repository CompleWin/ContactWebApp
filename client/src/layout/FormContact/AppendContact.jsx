import axios from "axios";
import FormContact from "./FormContact";
import { useNavigate } from 'react-router-dom';

const AppendContact = () => {

    const baseUrl = window.config.apiUrl;
    const navigate = useNavigate();


    const addContact = (contactName, contactPhone, contactEmail) => {

        const item = {
            name: contactName,
            phoneNumber: contactPhone,
            email: contactEmail
        }

        axios.post(`${baseUrl}/contacts`, item)
            .then(() => navigate("/"));
    }

    return (
        <div className="card">
            <div className="card-header">
                <h1>Добавить контакт</h1>
            </div>
            <div className="class-body">
                <FormContact addContact={addContact} />
            </div>
        </div>
    )
}

export default AppendContact;