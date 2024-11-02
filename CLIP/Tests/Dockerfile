FROM python:3.12

WORKDIR /

COPY . .

RUN /bin/sh -c wget https://repo.anaconda.com/miniconda/Miniconda3-latest-Linux-x86_64.sh # buildkit
RUN /bin/sh -c bash Miniconda3-latest-Linux-x86_64.sh -b -p /opt/miniconda # buildkit
RUN /bin/sh -c . /opt/miniconda/bin/activate # buildkit

RUN /bin/sh -c /opt/miniconda/bin/conda install -c pytorch faiss-cpu # buildkit
RUN /bin/sh -c /opt/miniconda/bin/conda install -c conda-forge libstdcxx-ng # buildkit
RUN /bin/sh -c export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:/opt/miniconda/lib # buildkit

RUN /bin/sh -c /opt/miniconda/bin/pip3 install wheel setuptools pip --upgrade # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install youtokentome # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install Cython # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install numpy # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install scikit-learn # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install pandas # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install transformers # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install Flask # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install ruclip==0.0.2 # buildkit
RUN /bin/sh -c /opt/miniconda/bin/pip3 install -U transformers # buildkit

RUN /bin/sh -c /opt/miniconda/bin/python3.12 -m test.py

CMD ["/opt/miniconda/bin/python3.12" "service.py"]