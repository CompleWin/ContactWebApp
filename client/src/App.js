import TableContact from "./layout/TableContact/TableContact";
import { useState, useEffect } from "react";
import FormContact from "./layout/FormContact/FormContact";
import axios from "axios";
import {Route, Routes, useLocation} from "react-router-dom";
import ContactDetails from "./layout/ContactDetails/ContactDetails";
import Pagination from "./layout/Pagination/Pagination";


const baseApiUrl = process.env.REACT_APP_API_URL;

const App = () => {

    const [contacts, setContacts] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [pageSize] = useState(10);
    const location = useLocation();

    const baseUrl = `${baseApiUrl}/contacts`;
    
    useEffect(() => {
        axios.get(baseUrl+`/page?pageNumber=${currentPage}&pageSize=${pageSize}`).then((response) => {
            setContacts(response.data.contacts);
            setTotalPages(Math.ceil(response.data.totalCount / pageSize));
        })
    }, [currentPage, pageSize, location.pathname])

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    }
    
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

        axios.post(baseUrl, item);
        
        axios.get(baseUrl+`/page?pageNumber=${currentPage}&pageSize=${pageSize}`).then(
            (response) => {
                setContacts(response.data.contacts);
                setTotalPages(Math.ceil(response.data.totalCount / pageSize));
            }
        )

    }

    const deleteContact = (id) => {
        setContacts(contacts.filter(item => item.id !== id));
        axios.delete(baseUrl + `/${id}`);
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
                            
                            <Pagination
                                currentPage={currentPage}
                                totalPages={totalPages}
                                onPageChange={handlePageChange}
                            ></Pagination>
                            
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
